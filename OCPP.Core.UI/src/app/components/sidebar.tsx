"use client";

import { Button } from "@/components/ui/button";

import { Separator } from "@/components/ui/separator";
import { cn } from "@/lib/utils";
import { buttonVariants } from "@/utils/motion";
import { AnimatePresence, motion } from "framer-motion";
import {
  ChevronDown,
  Clock,
  CreditCard,
  Feather,
  Home,
  MapPin,
  TrendingUp,
  Users,
  Zap,
} from "lucide-react";
import { usePathname, useRouter } from "next/navigation";
import React from "react";

interface SidebarProps extends React.HTMLAttributes<HTMLDivElement> {}

export const Sidebar = ({ className }: SidebarProps) => {
  const router = useRouter();
  const [showStationsMenu, setShowStationsMenu] = React.useState<boolean>(false);

  const pathName = usePathname();
  return (
    <div className={cn("pb-12", className)}>
      <div className="space-y-4 py-4">
        <div className="flex px-4 space-x-2">
          <Feather />
          <h2 className="mb-2 text-lg font-semibold tracking-tight">ChargeNow</h2>
        </div>
        <div className="px-3 py-2">
          <div className="space-y-1">
            <Button
              variant={pathName === "/dashboard" ? "secondary" : "ghost"}
              className="w-full justify-start"
              onClick={() => router.push("/dashboard")}
            >
              <Home className="mr-2 h-4 w-4" />
              Dashboard
            </Button>
            <motion.div whileHover={"hidden"} variants={buttonVariants}>
              <Button
                variant={pathName === "/stations" ? "secondary" : "ghost"}
                className="w-full justify-between peer group"
                onClick={() => {
                  router.push("/stations");
                  setShowStationsMenu(!showStationsMenu);
                }}
              >
                <div className="flex items-center">
                  <Zap className="mr-2 h-4 w-4" />
                  Stations
                </div>

                <ChevronDown
                  className={cn(
                    "opacity-0 group-hover:block group-hover:opacity-100 group-hover:transition-opacity group-hover:duration-500",
                    showStationsMenu ? "rotate-180" : "rotate-0"
                  )}
                />
              </Button>
            </motion.div>
            <AnimatePresence>
              {showStationsMenu && (
                <motion.div
                  key={"sub-menu-button"}
                  className="ml-3"
                  animate={{ y: [-5, 5, 0], opacity: [0, 0.5, 1] }}
                  exit={{ y: [0, 5, -20], opacity: [1, 0.5, 0.1] }}
                  transition={{ duration: 0.7 }}
                >
                  <Button
                    className="w-full justify-start"
                    variant={pathName === "/stations/locations" ? "secondary" : "ghost"}
                  >
                    <MapPin className="mr-2 h-4 w-4" />
                    Locations
                  </Button>
                  <Button
                    className="w-full justify-start"
                    variant={pathName === "/stations/sessions" ? "secondary" : "ghost"}
                  >
                    <Clock className="mr-2 h-4 w-4" />
                    Sessions
                  </Button>
                </motion.div>
              )}
            </AnimatePresence>
          </div>
        </div>
        <Separator className="w-11/12 mx-auto bg-border/50" />
        <div className="px-3 py-2">
          <div className="space-y-1">
            <Button
              variant={pathName === "/tariffs" ? "secondary" : "ghost"}
              className="w-full justify-start"
            >
              <CreditCard className="mr-2 h-4 w-4" />
              Tariffs
            </Button>
            <Button
              variant={pathName === "/reporting" ? "secondary" : "ghost"}
              className="w-full justify-start"
            >
              <TrendingUp className="mr-2 h-4 w-4" />
              Reporting
            </Button>
            <Button
              variant={pathName === "/drivers" ? "secondary" : "ghost"}
              className="w-full justify-start"
            >
              <Users className="mr-2 h-4 w-4" />
              Drivers
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};
