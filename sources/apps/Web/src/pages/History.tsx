import { useEffect, useState } from 'react'
import { MapContainer, TileLayer, Marker, Popup, useMap } from 'react-leaflet'
import L from "leaflet";
import icon from "leaflet/dist/images/marker-icon.png";
import iconShadow from "leaflet/dist/images/marker-shadow.png";
import 'leaflet/dist/leaflet.css'
import './../App.css'

delete (L.Icon.Default.prototype as any)._getIconUrl;
L.Icon.Default.mergeOptions({
  iconUrl: icon,
  shadowUrl: iconShadow,
});

const DEVICE_IDS = [
  "8343a9b1-ad5e-4fcb-9b80-4347229e2f17",
  "441cfb3d-9724-4606-a171-638e92545c47",
  "3e7e3dcc-c8b7-4c3e-9515-f1206fce6fbf",
  "c861e9f7-8aa8-4094-bab4-a505513bcd9b",
  "7043044c-b4e5-4cf4-a593-ad6c23813485",
  "09712c61-da66-4430-af42-d5cd61d2a5d1",
  "cba4e306-45c6-48a2-bfb2-50b24f9458d1",
  "6b625711-03db-4ae5-9b80-1755aee86123",
  "6d5ebf1e-cf87-4b13-8a80-65fbf8c67f21",
  "80807893-9a1a-42e7-a1d0-f55a0acc060f",
];

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

export default function History() {
  const [deviceId, setDeviceId] = useState<string>(DEVICE_IDS[0]);
  const [locations, setLocations] = useState<LocationDto[]>([]);

  useEffect(() => {
    fetch(`https://location-273348683080.europe-central2.run.app/api/device/${deviceId}/locations/history`)
      .then(res => res.json())
      .then(setLocations)
      .catch(console.error);
  }, [deviceId]);

  return (
    <div className='container'>
      <p className='title'>Lokalizacje urządzeń</p>

      <select value={deviceId} onChange={e => setDeviceId(e.target.value)}>
        {DEVICE_IDS.map(id => (
          <option key={id} value={id}>{id}</option>
        ))}
      </select>

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
