using UnityEngine;

public sealed class PlayerMover : MonoBehaviour
{
    [Header("Animation settings")]
    [SerializeField] private float movementSpeed;
    [Header("Particles")]
    [SerializeField] private ParticleSystem footstepParticles;
    
    private bool _isAllowedToMove;
    
    public void Init()
    {
        _isAllowedToMove = false;
    }

    private void Update()
    {
        if (_isAllowedToMove)
            Move();
    }

    public void SetMovementAllowance(bool isAllowedToMove)
    {
        _isAllowedToMove = isAllowedToMove;
       
        if (isAllowedToMove)
        {
            PopUpTextController.Instance.ShowText("Moving to next obstacles!");
            footstepParticles.Play((true));
        }
        else
        {
            footstepParticles.Stop(true);
        }
    }

    private void Move()
    {
        transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
    }
}
