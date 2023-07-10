"use client";

import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { cn } from "@/lib/utils";
import { MoreVertical } from "lucide-react";
import Link from "next/link";
import { usePathname } from "next/navigation";

export const StationLayout = ({ children }: { children: React.ReactNode }) => {
  const pathname = usePathname();
  return (
    <main className="col-span-4 p-5 space-y-5">
      <section className="flex justify-between">
        <div className="ml-4">
          <div className="flex gap-2 items-center">
            <h2 className="text-sm font-semibold">Parking Area 21</h2>
            <span className="relative flex h-[10px] w-[10px]">
              <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-green-400 opacity-75"></span>
              <span className="relative inline-flex rounded-full h-[10px] w-[10px] bg-green-500"></span>
            </span>
          </div>
          <p className="text-xs text-muted/80">Apartemen Gunawangsa, Surabaya</p>
        </div>
        <div className="flex text-sm gap-8">
          <Link
            href={"/stations/id"}
            className={cn(
              pathname.includes("stations") &&
                !pathname.includes("maintenance") &&
                !pathname.includes("sessions") &&
                "text-muted/80"
            )}
          >
            Overview
          </Link>
          <Link
            href={"/stations/id/maintenance"}
            className={cn(pathname.includes("maintenance") && "text-muted/80")}
          >
            Maintenance
          </Link>
          <Link
            href={"/stations/id/sessions"}
            className={cn(pathname.includes("sessions") && "text-muted/80")}
          >
            Sessions
          </Link>
        </div>
        <div className="flex space-x-2 items-center">
          <Button variant="ghost" className="h-10">
            <MoreVertical className="w-4 h-4" />
          </Button>
          <Button variant={"secondary"} className="h-10">
            Remote Actions
          </Button>
        </div>
      </section>
      <Separator className="my-5 bg-border/50" />
      {children}
    </main>
  );
};

export default StationLayout;
