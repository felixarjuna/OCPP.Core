import axios from "axios";
import { ChargePoint } from "./contracts";
// ----------------------------- Charge Point -----------------------------------
export const getChargePoints = async () => {
  const response = await axios.get<ChargePoint[]>("http://localhost:5207/api/chargepoints");
  return response.data;
};
