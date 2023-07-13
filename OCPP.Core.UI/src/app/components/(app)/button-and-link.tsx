"use client";

import { Button } from "@/components/ui/button";
import { ArrowRight } from "lucide-react";
import Link from "next/link";
import { useRouter } from "next/navigation";

export const ButtonAndLink = () => {
  const router = useRouter();
  return (
    <>
      <Button variant={"secondary"} onClick={() => router.push("/dashboard")}>
        Get started
      </Button>
      <Link href={""} className="hover:underline hover:underline-offset-4">
        <div className="flex gap-1 items-center">
          <p className="text-sm">Learn more</p>
          <ArrowRight className="w-4 h-4" />
        </div>
      </Link>
    </>
  );
};
