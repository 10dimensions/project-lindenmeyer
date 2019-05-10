namespace Mapbox.Examples
{
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using Mapbox.Utils;

	public class ImmediatePositionWithLocationProvider : MonoBehaviour
	{
		//[SerializeField]
		//private UnifiedMap _map;

		bool _isInitialized;

		public Vector2d ImmediateLocation;
		public Vector2d TargetLocation;
		private bool NearTarget = false;


		ILocationProvider _locationProvider;
		ILocationProvider LocationProvider
		{
			get
			{
				if (_locationProvider == null)
				{
					_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
				}

				return _locationProvider;
			}
		}

		Vector3 _targetPosition;

		void Start()
		{
			LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
		}

		void LateUpdate()
		{
			if (_isInitialized)
			{
				var map = LocationProviderFactory.Instance.mapManager;
				transform.localPosition = map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude);
				
				ImmediateLocation = LocationProvider.CurrentLocation.LatitudeLongitude;

				CheckForProximity();
			}
		}

		private void CheckForProximity()
		{
			if(NearTarget == false)
			{
				double _dist = Calc();
				print(_dist);
			}
		}

		public double Calc()
    	{
 
			var R = 6378.137; // Radius of earth in KM

			var dLat = (float)ImmediateLocation.x * Mathf.PI / 180 - (float)TargetLocation.x * Mathf.PI / 180;
			var dLon = (float)ImmediateLocation.y * Mathf.PI / 180 - (float)TargetLocation.y * Mathf.PI / 180;

			float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
						Mathf.Cos((float)ImmediateLocation.x * Mathf.PI / 180) * Mathf.Cos((float)TargetLocation.x * Mathf.PI / 180) *
						Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
			
			var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
			 
			var distance = R * c * 1000f; 

			return distance;
    	}


		void OnTriggerEnter(Collider coll)
		{
			SingletonAR.Instance.MeshName = coll.gameObject.tag;
		}

	}
}