﻿/*
 * This code is part of Arcade Car Physics for Unity by Saarg (2018)
 * 
 * This is distributed under the MIT Licence (see LICENSE.md for details)
 */
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace VehicleBehaviour.Utils {
	public class CameraFollow : MonoBehaviour {
		// Should the camera follow the target
		[SerializeField] bool follow = false;
		public bool Follow
		{
			get => follow;
			set => follow = value;
		}

		// Current target
		[SerializeField] Transform target = default;

		// Offset from the target position
		[SerializeField] Vector3 offset = -Vector3.forward;

		// Camera speeds
		[Range(0, 10)]
		[SerializeField] float lerpPositionMultiplier = 1f;
		[Range(0, 10)]		
		[SerializeField] float lerpRotationMultiplier = 1f;

		// We use a rigidbody to prevent the camera from going in walls but it means sometime it can get stuck
		Rigidbody rb;
		Rigidbody targetRb;

		void Start () {
			rb = GetComponent<Rigidbody>();
		}

		void FixedUpdate() {
			// If we don't follow or target is null return
			if (!follow || target == null) return;

			// normalise velocity so it doesn't jump too far
			this.rb.velocity.Normalize();

			// Save transform localy
			Quaternion curRot = transform.rotation;
			Vector3 tPos = target.position + target.TransformDirection(offset);

			// Look at the target
			transform.LookAt(target);

			// Keep the camera above the target y position
			if (tPos.y < target.position.y) {
				tPos.y = target.position.y;
			}

			// Set transform with lerp
			transform.position = Vector3.Lerp(transform.position, tPos, Time.fixedDeltaTime * lerpPositionMultiplier);
			transform.rotation = Quaternion.Lerp(curRot, transform.rotation, Time.fixedDeltaTime * lerpRotationMultiplier);

			// Keep camera above the y:0.5f to prevent camera going underground
			if ((transform.position.y - target.transform.position.y) < 0.5f) {
				transform.position = new Vector3(transform.position.x , (target.transform.position.y + 0.5f), transform.position.z);
			}
		}
	}
}