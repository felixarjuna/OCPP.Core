import axios from "axios";
import { ChargeStationForm } from "./contracts";

const apiClient = axios.create({
  baseURL: "http://localhost:5207",
});

// ----------------------------- Charge Point -----------------------------------
export const getChargeStations = async () => {
  const response = await apiClient.get<ChargeStationForm[]>("/api/chargestations");
  console.log(response.data);
  return response.data;
};

export const createChargeStation = async (request: ChargeStationForm) => {
  const response = await apiClient.post("/api/chargestations", request);
  return response.data;
};

export const deleteChargeStation = async (stationId: string) => {
  const response = await apiClient.delete(`/api/chargestations/${stationId}`);
  return response.data;
};
