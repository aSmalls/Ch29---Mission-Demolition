using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	static public bool 		goalMet = false;
	
	void OnTriggerEnter( Collider other ) {
		if (other.gameObject.tag == "Projectile") {
			Goal.goalMet = true;
			Color c = this.renderer.material.color;
			c.g = 0;
			c.r = 1;
			this.renderer.material.color = c;
		}
	}
}