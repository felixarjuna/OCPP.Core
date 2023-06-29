"use client";

import { Button } from "@/components/ui/button";
import { Zap } from "lucide-react";
import { useRouter } from "next/navigation";

export default function Home() {
  const router = useRouter();

  return (
    <main className="min-h-screen p-10">
      <div className="min-h-[calc(100vh-80px)] border rounded-xl p-8 flex items-center justify-center">
        <div className="flex flex-col space-y-6 justify-center items-center">
          <div className="flex items-center space-x-2">
            <h1 className="text-5xl ">Welcome to ChargeNow!</h1>
            <Zap className="h-10 w-10" />
          </div>

          <Button variant={"secondary"} className="w-fit" onClick={() => router.push("/dashboard")}>
            Dashboard
          </Button>
        </div>
      </div>
    </main>
  );
}
