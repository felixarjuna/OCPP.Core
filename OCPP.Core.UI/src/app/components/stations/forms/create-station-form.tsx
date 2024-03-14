"use client";

import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { useChargePointEvents } from "@/hooks/useChargePoints";
import { ChargeStationForm, addStationSchema } from "@/lib/contracts";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

export const CreateStationForm = () => {
  // Define the form
  const form = useForm<ChargeStationForm>({
    resolver: zodResolver(addStationSchema),
    defaultValues: {
      stationId: "",
      stationName: "",
      city: "",
      street: "",
      username: "",
      password: "",
      clientCertThumb: "",
    },
  });

  const { onCreateChargeStation } = useChargePointEvents(form.getValues().stationId);
  // Define submit handler
  const onSubmit = (values: ChargeStationForm) => {
    onCreateChargeStation.mutate(values);
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        <div className="flex flex-col space-y-8">
          <div className="space-y-4">
            <h2 className="font-bold text-lg">General Information</h2>
            <div className="grid grid-cols-3 gap-x-5 gap-y-3">
              <FormField
                control={form.control}
                name="stationId"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Station Id</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>

                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="stationName"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Station Name</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="city"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>City</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="street"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Street</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="username"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Username</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormDescription>(Optional) Used for authorization</FormDescription>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="password"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Password</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" type="password" />
                    </FormControl>
                    <FormDescription>(Optional) Used for authorization</FormDescription>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
          </div>

          <div className="space-y-4">
            <h2 className="font-bold text-lg">Charge Point Information</h2>
            <div className="grid grid-cols-3 gap-x-5 gap-y-3">
              <FormField
                control={form.control}
                name="model"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Charge Point Model</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="vendor"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Charge Point Vendor</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="chargePointSerialNumber"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Charge Point Serial Number</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="chargeBoxSerialNumber"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Charge Box Serial Number</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="firmwareVersion"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Firmware Version</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="meterSerialNumber"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Meter Serial Number</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="meterType"
                render={({ field }) => (
                  <FormItem className="col-span-1">
                    <FormLabel>Meter Type</FormLabel>
                    <FormControl>
                      <Input {...field} className="text-black text-sm" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
          </div>
        </div>
        <Button variant={"secondary"} type="submit">
          Add Station
        </Button>
      </form>
    </Form>
  );
};

export default CreateStationForm;
