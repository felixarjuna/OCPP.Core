"use client";

import { Button } from "@/components/ui/button";
import { Leaf } from "lucide-react";
import { useRouter } from "next/navigation";

export default async function Home() {
  const router = useRouter();
  return (
    <main className="p-10 flex items-center justify-center h-full flex-col space-y-7">
      <div className="flex items-center flex-col space-y-3">
        <h1 className="text-5xl gap-3 flex">
          Welcome to Ploeg!
          <Leaf className="w-10 h-10" />
        </h1>
        <p className="font-extralight">A place where all charge points find its home.</p>
      </div>
      <Button variant={"secondary"} onClick={() => router.push("/dashboard")}>
        Dashboard
      </Button>
    </main>
  );
}
