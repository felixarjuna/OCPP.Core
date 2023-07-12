"use client";

import { useToast } from "@/components/ui/use-toast";
import { createChargeStation, getChargeStations } from "@/lib/api";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

export const useChargePoints = () => {
  const { data: stations, isLoading } = useQuery({
    queryKey: ["chargestations"],
    queryFn: getChargeStations,
    initialData: [],
  });

  return { stations, isLoading };
};

export const useChargePointEvents = (stationId: string) => {
  const { toast } = useToast();

  const queryClient = useQueryClient();
  const onCreateChargeStation = useMutation({
    mutationKey: ["chargestations", stationId],
    mutationFn: createChargeStation,
    onSuccess: () => {
      queryClient.invalidateQueries(["chargestations"]);
      toast({
        title: "Oh yeah! 🚀",
        description: "Charge station successfully created!",
        variant: "secondary",
      });
    },
  });

  return { onCreateChargeStation };
};
