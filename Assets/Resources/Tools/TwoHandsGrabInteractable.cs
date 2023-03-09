using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandsGrabInteractable : XRGrabInteractable
{
    [SerializeField]
    private Transform _secondAttachTransform;

    //Activar el múltiple desde script ya que desde Unity no se puede
    private void Awake()
    {
        base.Awake();
        selectMode = InteractableSelectMode.Multiple;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {


        if (this.interactorsSelecting.Count == 1)
        {
            base.ProcessInteractable(updatePhase); //LLamar a la funcion process sin override
        }
        else if (this.interactorsSelecting.Count == 2
            && XRInteractionUpdateOrder.UpdatePhase.Dynamic == updatePhase) //Saber en que fase se encuentra, sobreescribir ProcessInteractable para cuando se agarre con 2 manos
        {
            ProcessTwoHandGrab();
        }

    }

    //Procesar
    private void ProcessTwoHandGrab()
    {
        Transform firstAttach = GetAttachTransform(null);
        //Coger el punto donde está la mano principal
        Transform firstHand = interactorsSelecting[0].transform;

        //Mano secundaria
        Transform secondAttach = _secondAttachTransform;
        //Coger el punto donde está la mano secundaria
        Transform secondHand = interactorsSelecting[1].transform;

        //Dirección entre las manos del usuario
        Vector3 directionBetweenHands = secondHand.position - firstHand.position;
        //Dirección entre los puntos de attach
        Vector3 directionBetweenAttach = secondAttach.position - firstAttach.position;

        //Rotación del arma dependiente de los puntos de attach
        Quaternion rotationFromAttachToForward = Quaternion.FromToRotation(directionBetweenAttach, this.transform.forward);

        //Hacia donde mirar y decir cual es su vector up (y), será el firstHand.up. Multiplicado por la rotación de los attach será como sumar ambas rotaciones
        Quaternion targetRotation = Quaternion.LookRotation(directionBetweenHands, firstHand.up) * rotationFromAttachToForward;

        //Vector desde Handle(Asa, Attach1) hasta el centro del arma
        Vector3 worldDirectionFromHandleToBase = transform.position - firstAttach.position;
        //Pasar coordenadas a local
        Vector3 localDirectionFromHandleToBase = transform.InverseTransformDirection(worldDirectionFromHandleToBase);

        //Vector desde el asa y rotar
        Vector3 targetPosition = firstHand.position + targetRotation * localDirectionFromHandleToBase;

        transform.SetPositionAndRotation(targetPosition, targetRotation);
    }


    protected override void Grab()
    {
        //Si se hace grab con 1 mano se llama a grab, si no no se hace
        if (interactorsSelecting.Count == 1)
        {
            base.Grab();
        }
    }

    //Resolver el problema de que cuando se agarre con dos manos y se suelta el arma se queda flotando
    protected override void Drop()
    {
        //Si no hay interactors sobre nuestro objeto. Se ha soltado, llamar a drop.
        if (!isSelected)
        {
            base.Drop();
        }
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        //Disparar solo con una de las manos, la de la posición 0
        if (interactorsSelecting[0] == args.interactorObject)
        {
            base.OnActivated(args);

        }
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        //Disparar solo con una de las manos, la de la posición 0
        if (interactorsSelecting[0] == args.interactorObject)
        {
            base.OnDeactivated(args);
        }
    }
}
