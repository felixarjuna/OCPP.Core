"use client";

import { getChargePoints } from "@/lib/api";
import { useQuery } from "@tanstack/react-query";

export const useChargePoints = () => {
  const { data: chargePoints, isLoading } = useQuery({
    queryKey: ["chargePoints"],
    queryFn: getChargePoints,
    initialData: [],
  });

  return { chargePoints, isLoading };
};
