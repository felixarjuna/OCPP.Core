import { z } from "zod";

export type ChargeStation = z.infer<typeof chargeStationSchema>;

export const chargeStationSchema = z.object({
  stationId: z.string().min(2, { message: "Station Id must be at least 2 characters." }),
  stationName: z.string().min(2, { message: "Station name must be at least 2 characters." }),
  city: z.string().min(2, { message: "City must be at least 2 characters. " }),
  street: z.string().min(2, { message: "City must be at least 2 characters. " }),
  username: z.string(),
  password: z.string(),
  clientCertThumb: z.string(),
});
