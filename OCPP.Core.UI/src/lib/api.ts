import axios from "axios";
import { ChargeStation } from "./contracts";
// ----------------------------- Charge Point -----------------------------------
export const getChargePoints = async () => {
  const response = await axios.get<ChargeStation[]>("http://localhost:5207/api/chargepoints");
  return response.data;
};

export const createChargePoint = async (request: ChargeStation) => {
  console.log(request);
  const response = await axios.post("http://localhost:5207/api/chargepoints", request);
  return response.data;
};
