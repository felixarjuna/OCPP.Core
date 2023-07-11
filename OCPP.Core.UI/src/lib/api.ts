import axios from "axios";
import { ChargeStation } from "./contracts";
// ----------------------------- Charge Point -----------------------------------
export const getChargePoints = async () => {
  const response = await axios.get<ChargeStation[]>("http://localhost:5207/api/chargepoints");
  return response.data;
};

export const createChargePoint = async (request: ChargeStation) => {
  const response = await axios.post("http://localhost:5207/api/chargepoints", request);
  return response.data;
};

export const deleteChargePoint = async (stationId: string) => {
  const response = await axios.delete(`http://localhost:5207/api/chargepoints/${stationId}`);
  return response.data;
};
