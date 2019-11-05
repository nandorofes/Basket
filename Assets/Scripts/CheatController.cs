using UnityEngine;
using System.Collections;

public class CheatController : MonoBehaviour
{
    public CreditsManagement creditsManagement;

    private void Start()
    {
      
            if (this.creditsManagement != null)
                this.creditsManagement.credits = 10;

			GameManager.Instance.GamePersistentData.Tickets =  100000;
    }
	
}