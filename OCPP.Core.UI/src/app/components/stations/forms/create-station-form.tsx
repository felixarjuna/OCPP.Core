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
import { ChargeStation, chargeStationSchema } from "@/lib/contracts";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

export const CreateStationForm = () => {
  // Define the form
  const form = useForm<ChargeStation>({
    resolver: zodResolver(chargeStationSchema),
    defaultValues: {
      chargePointId: "",
      name: "",
      comment: "",
      username: "",
      password: "",
      clientCertThumb: "",
    },
  });

  const { onCreateChargeStation } = useChargePointEvents(form.getValues().chargePointId);
  // Define submit handler
  const onSubmit = (values: ChargeStation) => {
    onCreateChargeStation.mutate(values);
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        <div className="grid grid-cols-2 gap-x-5 gap-y-3">
          <FormField
            control={form.control}
            name="chargePointId"
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
            name="name"
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
          <FormField
            control={form.control}
            name="clientCertThumb"
            render={({ field }) => (
              <FormItem className="col-span-1">
                <FormLabel>Client Certificate</FormLabel>
                <FormControl>
                  <Input {...field} className="text-black text-sm" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="comment"
            render={({ field }) => (
              <FormItem className="col-span-1">
                <FormLabel>Comment</FormLabel>
                <FormControl>
                  <Input {...field} className="text-black text-sm" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        </div>
        <Button variant={"secondary"} type="submit">
          Submit
        </Button>
      </form>
    </Form>
  );
};

export default CreateStationForm;
