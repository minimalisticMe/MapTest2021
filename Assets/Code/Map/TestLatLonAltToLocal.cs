using Microsoft.Geospatial;
using Microsoft.Maps.Unity;
using UnityEngine;

public class TestLatLonAltToLocal : MonoBehaviour
{
    // Assign some sort of prefab to this. I used sphere scaled down to 0.025.
    [SerializeField]
    private GameObject _someSortOfPrefab;

    // The following code adds a 2d-grid of GameObjects to the map. Their positions are computed by transforming LatLonAlt to local space.
    // LateUpdate is used to ensure MapRenderer has had one Update cycle to set it's initial Bounds.
    void LateUpdate()
    {
        var mapRenderer = GetComponent<MapRenderer>();

        var bounds = mapRenderer.Bounds;
        if (bounds.BottomLeft.LongitudeInDegrees > bounds.TopRight.LongitudeInDegrees)
        {
            Debug.LogError("Following code does not work when bounds crosses the anti-meridian. Zoom in.");
            return;
        }

        var mercatorBounds = bounds.ToMercatorBoundingBox();
        var mercatorWidth = mercatorBounds.Width;
        var mercatorHeight = mercatorBounds.Height;

        var samplesPerSide = 10;
        var mercatorXIncrement = mercatorWidth / samplesPerSide;
        var mercatorYIncrement = mercatorHeight / samplesPerSide;

        for (var x = 0; x <= samplesPerSide; x++)
        {
            for (var y = 0; y <= samplesPerSide; y++)
            {
                // Derive the LatLonAlt of this sample.
                var mercatorCoord =
                    new MercatorCoordinate(
                        mercatorBounds.BottomLeft.X + x * mercatorXIncrement,
                        mercatorBounds.BottomLeft.Y + y * mercatorYIncrement);
                //var latLon = mercatorCoord.ToLatLon();
                var latitude = 49.42928658731564d;
                var longitude = 7.779350280761719d;
                var latLon = new LatLon(latitude, longitude);
                var latLonAlt = new LatLonAlt(latLon, 0);
                var localSpacePosition = mapRenderer.TransformLatLonAltToLocalPoint(latLonAlt);
                Debug.Log($"{latLon.LatitudeInDegrees}, {latLon.LongitudeInDegrees} => ({localSpacePosition.x}, {localSpacePosition.y}, {localSpacePosition.z})");

                // Instantiate a new GO, set parent to the MapRenderer, and set local space position.
                // Be sure to set 'transform.localPosition' on the GO below, not 'transform.position' which is world space.
                var go = Instantiate(_someSortOfPrefab);
                go.transform.SetParent(mapRenderer.transform, false);
                go.transform.localPosition = localSpacePosition;
            }
        }

        Destroy(this);
    }
}
