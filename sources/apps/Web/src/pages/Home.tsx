import { useEffect, useState } from 'react'
import { MapContainer, TileLayer, Marker, Popup, useMap } from 'react-leaflet'
import L from "leaflet";
import iconUrl from "leaflet/dist/images/marker-icon.png";
import iconShadowUrl from "leaflet/dist/images/marker-shadow.png";
import 'leaflet/dist/leaflet.css'
import './../App.css'

L.Icon.Default.mergeOptions({
  iconUrl: iconUrl,
  shadowUrl: iconShadowUrl,
  iconRetinaUrl: iconUrl,
});

const API_URL = "https://location-273348683080.europe-central2.run.app/api/device/locations";

type LocationDto = {
  deviceId: string;
  latitude: number;
  longitude: number;
  timestampUtc: string;
};

function SetView({ center, zoom }: { center: [number, number]; zoom: number }) {
  const map = useMap();
  map.setView(center, zoom);
  return null;
}

function Home() {
  const [locations, setLocations] = useState<LocationDto[]>([]);

  useEffect(() => {
    fetch(API_URL)
      .then(res => res.json() as Promise<LocationDto[]>)
      .then(setLocations)
      .catch(console.error);
  }, []);

  return (
    <div className='container'>
      <p className='title'>Lokalizacje urządzeń</p>

      <MapContainer style={{ height: "100%", width: "100%" }}>
        <SetView center={[50.06689025502986, 19.91303407039251]} zoom={12} />
        <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />

        {locations.map(loc => (
          <Marker
            key={`${loc.deviceId}-${loc.timestampUtc}`}
            position={[loc.latitude, loc.longitude]}
          >
            <Popup>
              <b>Device:</b> {loc.deviceId}<br />
              <b>Lat:</b> {loc.latitude}<br />
              <b>Lng:</b> {loc.longitude}<br />
              <b>Time:</b> {new Date(loc.timestampUtc).toLocaleString()}
            </Popup>
          </Marker>
        ))}
      </MapContainer>
    </div>
  );
}

export default Home;
