using UnityEngine;
using System.Collections;

/// <summary>
/// You can user default trail renderer if you dont need your tracks to fade slowly
/// </summary>
public class TimedTrailRenderer : MonoBehaviour
{
    public Terrain terrain;
    public Camera touchReactCam;

    public bool emit = true;

    public Material material;

    public bool ignorelifeTime;
    public float lifeTime = 1.00f;

    public Color[] colors;
    public float[] sizes;

    public float uvLengthScale = 0.01f;
    public bool higherQualityUVs = true;

    public int movePixelsForRebuild = 6;
    public float maxRebuildTime = 0.1f;

    public float minVertexDistance = 0.10f;

    public float maxVertexDistance = 10.00f;
    public float maxAngle = 3.00f;

    public bool autoDestruct = false;

    private ArrayList points = new ArrayList();
    private GameObject o;
    private Vector3 lastPosition;
    private Vector3 lastCameraPosition1;
    private Vector3 lastCameraPosition2;
    private float lastRebuildTime = 0.00f;
    private bool lastFrameEmit = true;

    [SerializeField] private bool isPhysics = true;

    public class Point
    {
        public float timeCreated = 0.00f;
        public Vector3 position;
        public bool lineBreak = false;
    }

    void Start()
    {
        lastPosition = transform.position;
        o = new GameObject("Trail");
        o.transform.parent = null;
        o.transform.position = Vector3.zero;
        o.transform.rotation = Quaternion.identity;
        o.transform.localScale = Vector3.one;
        o.AddComponent(typeof(MeshFilter));
        o.AddComponent(typeof(MeshRenderer));
        o.GetComponent<Renderer>().sharedMaterial = material;
        o.transform.gameObject.layer = LayerMask.NameToLayer("TouchReact");
    }

    void OnDisable()
    {
        Destroy(o);
    }

    void Update()
    {
        if (!emit && points.Count == 0 && autoDestruct)
        {
            Destroy(o);
            Destroy(gameObject);
        }

        // early out if there is no camera
        if (!touchReactCam) return;

        bool re = false;

        // if we have moved enough, create a new vertex and make sure we rebuild the mesh
        float theDistance = (lastPosition - transform.position).magnitude;
        if (emit)
        {
            if (theDistance > minVertexDistance)
            {
                bool make = false;
                if (points.Count < 3)
                {
                    make = true;
                }
                else
                {
                    Vector3 l1 = ((Point)points[points.Count - 2]).position - ((Point)points[points.Count - 3]).position;
                    Vector3 l2 = ((Point)points[points.Count - 1]).position - ((Point)points[points.Count - 2]).position;
                    if (Vector3.Angle(l1, l2) > maxAngle || theDistance > maxVertexDistance) make = true;
                }

                if (make)
                {
                    Point p = new Point();
                    p.position = transform.position;
                    p.timeCreated = Time.time;
                    points.Add(p);
                    lastPosition = transform.position;
                }
                else
                {
                    ((Point)points[points.Count - 1]).position = transform.position;
                    ((Point)points[points.Count - 1]).timeCreated = Time.time;
                }
            }
            else if (points.Count > 0)
            {
                ((Point)points[points.Count - 1]).position = transform.position;
                ((Point)points[points.Count - 1]).timeCreated = Time.time;
            }
        }

        if (!emit && lastFrameEmit && points.Count > 0) ((Point)points[points.Count - 1]).lineBreak = true;
        lastFrameEmit = emit;

        // approximate if we should rebuild the mesh or not
        if (points.Count > 1)
        {
            Vector3 cur1 = touchReactCam.WorldToScreenPoint(((Point)points[0]).position);
            lastCameraPosition1.z = 0;
            Vector3 cur2 = touchReactCam.WorldToScreenPoint(((Point)points[points.Count - 1]).position);
            lastCameraPosition2.z = 0;

            float distance = (lastCameraPosition1 - cur1).magnitude;
            distance += (lastCameraPosition2 - cur2).magnitude;

            if (distance > movePixelsForRebuild || Time.time - lastRebuildTime > maxRebuildTime)
            {
                re = true;
                lastCameraPosition1 = cur1;
                lastCameraPosition2 = cur2;
            }
        }
        else
        {
            re = true;
        }


        if (re)
        {
            lastRebuildTime = Time.time;

            ArrayList remove = new ArrayList();
            int i = 0;
            foreach (Point p in points)
            {
                // cull old points first
                if (Time.time - p.timeCreated > lifeTime && !ignorelifeTime) remove.Add(p);
                i++;
            }

            foreach (Point p in remove) points.Remove(p);
            remove.Clear();

            if (points.Count > 1)
            {
                Vector3[] newVertices = new Vector3[points.Count * 2];
                Vector2[] newUV = new Vector2[points.Count * 2];
                int[] newTriangles = new int[(points.Count - 1) * 6];
                Color[] newColors = new Color[points.Count * 2];

                i = 0;
                float curDistance = 0.00f;

                foreach (Point p in points)
                {
                    float time = 0;
                    if (!ignorelifeTime) time = (Time.time - p.timeCreated) / lifeTime;
                    float relativePosition = p.position.y - Terrain.activeTerrain.SampleHeight(p.position);
                    //Debug.Log("Point "+ i +" position: " + p.position + " | Terrain position height: " + Terrain.activeTerrain.SampleHeight(p.position) + " | Relative position height: " + (p.position.y - Terrain.activeTerrain.SampleHeight(p.position)));
                    time = time + relativePosition-0.5f;
                    if (relativePosition >= 1 || relativePosition < 0) time = 1;

                    Color color = Color.Lerp(Color.white, Color.clear, time);
                    if (colors != null && colors.Length > 0)
                    {
                        float colorTime = time * (colors.Length - 1);
                        float min = Mathf.Floor(colorTime);
                        float max = Mathf.Clamp(Mathf.Ceil(colorTime), 1, colors.Length - 1);
                        float lerp = Mathf.InverseLerp(min, max, colorTime);
                        if (min >= colors.Length) min = colors.Length - 1; if (min < 0) min = 0;
                        if (max >= colors.Length) max = colors.Length - 1; if (max < 0) max = 0;
                        color = Color.Lerp(colors[(int)min], colors[(int)max], lerp);
                    }

                    float size = 1f;
                    if (sizes != null && sizes.Length > 0)
                    {
                        float sizeTime = time * (sizes.Length - 1);
                        float min = Mathf.Floor(sizeTime);
                        float max = Mathf.Clamp(Mathf.Ceil(sizeTime), 1, sizes.Length - 1);
                        float lerp = Mathf.InverseLerp(min, max, sizeTime);
                        if (min >= sizes.Length) min = sizes.Length - 1; if (min < 0) min = 0;
                        if (max >= sizes.Length) max = sizes.Length - 1; if (max < 0) max = 0;
                        size = Mathf.Lerp(sizes[(int)min], sizes[(int)max], lerp);
                    }

                    Vector3 lineDirection = Vector3.zero;
                    if (i == 0) lineDirection = p.position - ((Point)points[i + 1]).position;
                    else lineDirection = ((Point)points[i - 1]).position - p.position;

                    Vector3 vectorToCamera = touchReactCam.transform.position - p.position;
                    Vector3 perpendicular = Vector3.Cross(lineDirection, vectorToCamera).normalized;
                    //Vector3 perpendicular = Vector3.back;
                    newVertices[i * 2] = p.position + (perpendicular * (size * 0.8f));
                    newVertices[(i * 2) + 1] = p.position + (-perpendicular * (size * 0.8f));

                    newColors[i * 2] = newColors[(i * 2) + 1] = color;

                    newUV[i * 2] = new Vector2(curDistance * uvLengthScale, 0);
                    newUV[(i * 2) + 1] = new Vector2(curDistance * uvLengthScale, 1);

                    if (i > 0 && !((Point)points[i - 1]).lineBreak)
                    {
                        if (higherQualityUVs) curDistance += (p.position - ((Point)points[i - 1]).position).magnitude;
                        else curDistance += (p.position - ((Point)points[i - 1]).position).sqrMagnitude;

                        newTriangles[(i - 1) * 6] = (i * 2) - 2;
                        newTriangles[((i - 1) * 6) + 1] = (i * 2) - 1;
                        newTriangles[((i - 1) * 6) + 2] = i * 2;

                        newTriangles[((i - 1) * 6) + 3] = (i * 2) + 1;
                        newTriangles[((i - 1) * 6) + 4] = i * 2;
                        newTriangles[((i - 1) * 6) + 5] = (i * 2) - 1;
                    }

                    i++;
                }

                Mesh mesh = (o.GetComponent(typeof(MeshFilter)) as MeshFilter).mesh;
                mesh.Clear();
                mesh.vertices = newVertices;
                mesh.colors = newColors;
                mesh.uv = newUV;
                mesh.triangles = newTriangles;
            }
        }
    }

    public bool getHeight(Vector3 pos,out float y)
    {
        y = 1f;
        if (!isPhysics) return false;
        if (points.Count < 2) return false;

        float aux = 0f;
        int index = 0;
        Vector3 posFix = new Vector3(pos.x, 0, pos.z);
        Vector3 point1Fix = new Vector3(((Point)points[0]).position.x,0, ((Point)points[0]).position.z);
        Vector3 point2Fix = new Vector3(((Point)points[1]).position.x, 0, ((Point)points[1]).position.z);
        float minDistance = UnityEditor.HandleUtility.DistancePointLine(posFix, point1Fix, point2Fix);
        for (int i = 1; i < points.Count-1; i++)
        {
            point1Fix = new Vector3(((Point)points[i]).position.x, 0, ((Point)points[i]).position.z);
            point2Fix = new Vector3(((Point)points[i+1]).position.x, 0, ((Point)points[i+1]).position.z);
            aux = UnityEditor.HandleUtility.DistancePointLine(posFix, point1Fix, point2Fix);
            if (aux < minDistance)
            {
                minDistance = aux;
                index = i;
            }
        }

        if (minDistance > 0.5f) return false;
        Point p1 = ((Point)points[index]);
        Point p2 = ((Point)points[index + 1]);

        float aDistance = Vector3.Distance(pos, p1.position);
        float bDistance = Vector3.Distance(pos, p2.position);

        float time1 = 0;
        if (!ignorelifeTime) time1 = (Time.time - p1.timeCreated) / lifeTime;
        float relativePosition1 = p1.position.y - Terrain.activeTerrain.SampleHeight(p1.position);
        time1 = time1 + relativePosition1 - 0.5f;
        if (relativePosition1 >= 1 || relativePosition1 < 0) time1 = 1;
        //print(time1);
        float time2 = 0;
        if (!ignorelifeTime) time2 = (Time.time - p2.timeCreated) / lifeTime;
        float relativePosition2 = p2.position.y - Terrain.activeTerrain.SampleHeight(p2.position);
        time2 = time2 + relativePosition2 - 0.5f;
        if (relativePosition2 >= 1 || relativePosition2 < 0) time2 = 1;

        y = Mathf.Lerp(time1, time2, aDistance/(aDistance+bDistance));
        //y = GetClosestPointOnFiniteLine(pos, ((Point)points[index]).position, ((Point)points[index + 1]).position).y;
        return true;
    }

    Vector3 GetClosestPointOnFiniteLine(Vector3 point, Vector3 line_start, Vector3 line_end)
    {
        Vector3 line_direction = line_end - line_start;
        float line_length = line_direction.magnitude;
        line_direction.Normalize();
        float project_length = Mathf.Clamp(Vector3.Dot(point - line_start, line_direction), 0f, line_length);
        return line_start + line_direction * project_length;
    }
}