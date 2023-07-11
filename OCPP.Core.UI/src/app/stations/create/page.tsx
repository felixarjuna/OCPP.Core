"use client";

import CreateStationForm from "@/app/components/stations/forms/create-station-form";
import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { ArrowLeft } from "lucide-react";
import { useRouter } from "next/navigation";

export const Home = () => {
  const router = useRouter();
  return (
    <main className="col-span-4 p-5 space-y-5">
      <div>
        <div className="flex justify-between items-center">
          <h1 className="font-bold ml-4">Add Charge Station</h1>
          <Button
            variant="secondary"
            className="gap-1"
            onClick={() => {
              router.back();
            }}
          >
            <ArrowLeft className="w-4 h-4" />
            <p>Back</p>
          </Button>
        </div>
        <Separator className="my-5 bg-border/50" />
      </div>
      <div>
        <CreateStationForm />
      </div>
    </main>
  );
};

export default Home;
