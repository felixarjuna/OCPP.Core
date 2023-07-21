import { z } from "zod";

export type ChargeStationForm = z.infer<typeof addStationSchema>;

export const addStationSchema = z.object({
  stationId: z.string().min(2, { message: "Station Id must be at least 2 characters." }),
  stationName: z.string().min(2, { message: "Station name must be at least 2 characters." }),
  city: z.string().min(2, { message: "City must be at least 2 characters. " }),
  street: z.string().min(2, { message: "City must be at least 2 characters. " }),
  username: z.string(),
  password: z.string(),
  clientCertThumb: z.string(),
});

const chargeStationSchema = z.object({
  stationId: z.string(),
  stationName: z.string(),

  serialNumber: z.string().nullable(),
  model: z.string().nullable(),
  vendorName: z.string().nullable(),
  firmwareVersion: z.string().nullable(),
  modem: z.union([z.literal("iccid"), z.literal("imsi")]).nullable(),

  username: z.string().optional(),
  password: z.string().optional(),
  clientCertThumb: z.string().optional(),

  city: z.string(),
  street: z.string(),

  online: z.boolean(),
  protocol: z.string().nullable(),

  connectors: z.array(z.string()).nullable(),
  energy: z.number(),
  money: z.number(),
  transactions: z.number(),
});

export type ChargeStation = z.infer<typeof chargeStationSchema>;
