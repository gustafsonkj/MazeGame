using UnityEngine;
using System.Collections;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class RollerBall : MonoBehaviour {

	public GameObject ViewCamera = null;
	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip CoinSound = null;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float dampSpeed = 15.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
	private Rigidbody mRigidBody = null;
	private AudioSource mAudioSource = null;
	private bool mFloorTouched = false;

	void Start () {
		mRigidBody = GetComponent<Rigidbody> ();
		mAudioSource = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {
        //float moveHorizontal = Input.GetAxis("Horzontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, moveVertical);

		if (mRigidBody != null)
        {
			if (Input.GetButton ("Horizontal"))
            {
                //mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal")*10);
                transform.Translate(Vector3.right * Input.GetAxis("Horizontal") / dampSpeed);
                //if (Input.GetButton("Fire3"))
                //{
                //    transform.Translate(Vector3.right * Input.GetAxis("Horizontal") / (dampSpeed / 2));
                //}
			}
			if (Input.GetButton ("Vertical"))
            {
                //mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical")*10);
                transform.Translate(Vector3.forward * Input.GetAxis("Vertical") / dampSpeed);
                if (Input.GetButton("Fire3"))
                {
                    transform.Translate(Vector3.forward * Input.GetAxis("Vertical") / (dampSpeed / 1.2f));
                }
            }
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
			//if (Input.GetButtonDown("Jump")) {
			//	if(mAudioSource != null && JumpSound != null){
			//		mAudioSource.PlayOneShot(JumpSound);
			//	}
			//	mRigidBody.AddForce(Vector3.up*200);
			//}
		}
        if (ViewCamera != null)
        {
            //Vector3 direction = (Vector3.up * 2 + Vector3.back) * 2;
            //RaycastHit hit;
            //Debug.DrawLine(transform.position, transform.position + direction, Color.red);
            //if (Physics.Linecast(transform.position, transform.position + direction, out hit))
            //{
            //    ViewCamera.transform.position = hit.point;
            //}
            //else
            //{
            //    ViewCamera.transform.position = transform.position + direction;
            //}
            //ViewCamera.transform.LookAt(transform.position);
            ViewCamera.transform.position = transform.position;
            ViewCamera.transform.rotation = transform.rotation;
        }
    }

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = true;
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		} else {
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		}
	}

	void OnCollisionExit(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Coin")) {
			if(mAudioSource != null && CoinSound != null){
				mAudioSource.PlayOneShot(CoinSound);
			}
			Destroy(other.gameObject);
		}
	}
}
