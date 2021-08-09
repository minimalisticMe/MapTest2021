using Microsoft.Geospatial;
using Microsoft.Maps.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Microsoft_Map : MonoBehaviour
{
    public Image image;
    public Image image2;
    public MapPin mapPin;
    public MapRenderer mapRenderer;

    private float timer;
    private float updateInterval = 1f;
    private MapPinLayer mapPinLayer = new MapPinLayer();

    void Start()
    {
        var latitude = 49.42928658731564d;
        var longitude = 7.779350280761719d;
        var latlon = new LatLon(latitude, longitude);
        var latlonAlt = new LatLonAlt(latlon, 0);

        //image = transform.Find("Canvas/Pin").GetComponent<Image>();

        var pos2 = MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(mapRenderer, latlonAlt);
        //pos2.z = -2;
        var pos3 = MapRendererTransformExtensions.TransformLatLonAltToWorldPoint(mapRenderer, latlonAlt);
        //pos3.z = -2;

        image.transform.position = pos2;
        image2.transform.position = pos3;

        Debug.Log(pos2.ToString());
        Debug.Log(pos3.ToString());

        // var width = Screen.width;
        // var height = Screen.height;
        // Debug.Log("Width: " + width + " (half: " + width/2 + ")");
        // Debug.Log("Height: " + height + " (half: " + height/2 + ")");

        // var pos4 = pos2;
        // pos4.x = width/2;
        // pos4.y = height/2;
        // Debug.Log(pos4.ToString());
        // image2.transform.position = pos4;
        
        // var pos5 = pos2;
        // pos5.x = width/2 + pos2.x * width;
        // pos5.y = height/2 + pos2.y * height;
        // Debug.Log(pos5.ToString());
        // image.transform.position = pos5;

        var mapRenderer3 = this.transform.gameObject.GetComponent<MapRenderer>();
        var newGo3 = new GameObject();
        var mapPin3= newGo3.AddComponent<MapPin>();
        mapPin3.Location = latlon;
        newGo3.transform.parent = mapRenderer3.transform;
        Debug.Log("MapPin3: " + mapPin3.PositionInMapLocalSpace.ToString());
        Debug.Log("MapPin3 (lat/lon): " + latlon.ToString());
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += updateInterval;

            var latitude = 49.42928658731564d;
            var longitude = 7.779350280761719d;
            var latlon = new LatLon(latitude, longitude);
            var latlonAlt = new LatLonAlt(latlon, 0);

            var pos2 = MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(mapRenderer, latlonAlt);
            var pos3 = MapRendererTransformExtensions.TransformLatLonAltToWorldPoint(mapRenderer, latlonAlt);

            // image.transform.position = pos2;
            // image2.transform.position = pos3;

            // Debug.Log(pos2.ToString());
            // Debug.Log(pos3.ToString());

            // mapPin.Location = latlon;

            // var mapPinList = new List<MapPin>();
            // mapPinList.Add(mapPin);

            // MapPin.SynchronizeLayers(mapPinList, mapRenderer);
            // MapPin.UpdateScales(mapPinList, mapRenderer);
            // var localSpace = mapPin.PositionInMapLocalSpace;
            // Debug.Log("MapPin: " + localSpace.ToString());


            // var newGo2 = new GameObject();
            // var mapPin2 = newGo2.AddComponent<MapPin>();
            // mapPin2.Location = latlon;
            // mapPin2.LocationChanged += LocChanged;
            // newGo2.transform.parent = mapRenderer.transform;
            // Debug.Log("MapPin2: " + mapPin2.PositionInMapLocalSpace.ToString());
            // Debug.Log("MapPin2 (lat/lon): " + latlon.ToString());
        }
    }

    private void LocChanged(MapPin mapPin, LatLon latlon)
    {
        Debug.Log("MapPin LocationChanged: " + mapPin.PositionInMapLocalSpace.ToString());
        Debug.Log("MapPin LocationChanged (lat/lon): " + latlon.ToString());
    }
}
