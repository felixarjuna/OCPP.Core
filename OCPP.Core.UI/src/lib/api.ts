import axios from "axios";
import { ChargeStation } from "./contracts";
// ----------------------------- Charge Point -----------------------------------
export const getChargeStations = async () => {
  const response = await axios.get<ChargeStation[]>("http://localhost:5207/api/chargestations");
  return response.data;
};

export const createChargeStation = async (request: ChargeStation) => {
  const response = await axios.post("http://localhost:5207/api/chargestations", request);
  return response.data;
};

export const deleteChargeStation = async (stationId: string) => {
  const response = await axios.delete(`http://localhost:5207/api/chargestations/${stationId}`);
  return response.data;
};
