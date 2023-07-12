import { z } from "zod";

export type ChargeStation = z.infer<typeof chargeStationSchema>;

export const chargeStationSchema = z.object({
  chargeStationId: z.string().min(2, { message: "Station Id must be at least 2 characters." }),
  name: z.string().min(2, { message: "Station name must be at least 2 characters." }),
  comment: z.string(),
  username: z.string(),
  password: z.string(),
  clientCertThumb: z.string(),
});
