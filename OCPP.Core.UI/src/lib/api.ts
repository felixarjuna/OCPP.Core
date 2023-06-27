import axios from "axios";
// ----------------------------- Charge Point -----------------------------------
const getChargePoints = async () => {
  const response = await axios.get("");
  return response.data;
};
