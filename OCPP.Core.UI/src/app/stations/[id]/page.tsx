"use client";

import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { MoreVertical } from "lucide-react";
import Image from "next/image";

export const Home = () => {
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
          <p className="text-muted/80">Overview</p>
          <p>Maintenance</p>
          <p>Sessions</p>
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
      <section className="space-y-8">
        <div className="grid grid-cols-2 space-x-5">
          <div>
            <div className="col-span-1">
              <h1 className="font-bold ml-4">Station Connectors</h1>
              <Separator className="my-5 bg-border/50" />
            </div>

            <div className="flex flex-col space-y-5">
              <div className="flex space-x-4">
                <div className="w-12 h-12 rounded-full bg-blue-500 flex items-center justify-center">
                  <Image src="/connector.svg" alt="" width={25} height={25} />
                </div>
                <div className="my-auto">
                  <h4 className="text-muted text-xs">EVSE One</h4>
                  <div className="flex items-center space-x-4">
                    <h2 className="font-bold ">Charging</h2>
                    <p className="text-xs">200kW delivered | Type 2</p>
                  </div>
                </div>
              </div>
              <div className="flex space-x-4">
                <div className="w-12 h-12 rounded-full bg-blue-500 flex items-center justify-center">
                  <Image src="/connector.svg" alt="" width={25} height={25} />
                </div>
                <div className="my-auto">
                  <h4 className="text-muted text-xs">EVSE Two</h4>
                  <div className="flex items-center space-x-4">
                    <h2 className="font-bold ">Charging</h2>
                    <p className="text-xs">40kW delivered | Type 2</p>
                  </div>
                </div>
              </div>
              <div className="flex space-x-4">
                <div className="w-12 h-12 rounded-full bg-green-500 flex items-center justify-center">
                  <Image src="/connector.svg" alt="" width={25} height={25} />
                </div>
                <div className="my-auto">
                  <h4 className="text-muted text-xs">EVSE Three</h4>
                  <div className="flex items-center space-x-4">
                    <h2 className="font-bold">Available</h2>
                    <p className="text-xs">Last used 15:30, 10th July 2023</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div>
            <div className="col-span-1">
              <h1 className="font-bold ml-4">Station Performance</h1>
              <Separator className="my-5 bg-border/50" />
            </div>
            <div className="grid grid-cols-2 gap-5 items-center justify-center">
              <div className="col-span-1 border-2 h-24 rounded-lg p-4">
                <p className="text-xs font-bold">Money Generated</p>
                <h2 className="text-xl mt-1">Rp. 2.000.000</h2>
                <p className="text-xs">10% Increase</p>
              </div>
              <div className="col-span-1 border-2 h-24 rounded-lg p-4">
                <p className="text-xs font-bold">Charge Sessions</p>
                <h2 className="text-xl mt-1">324</h2>
                <p className="text-xs">81% Increase</p>
              </div>
              <div className="col-span-1 border-2 h-24 rounded-lg p-4">
                <p className="text-xs font-bold">Energy Delivered</p>
                <h2 className="text-xl mt-1">492kWh</h2>
                <p className="text-xs">39% Increase</p>
              </div>
              <div className="col-span-1 border-2 h-24 rounded-lg p-4">
                <p className="text-xs font-bold">Station Uptime</p>
                <h2 className="text-xl mt-1">98.5%</h2>
                <p className="text-xs">13% Increase</p>
              </div>
            </div>
          </div>
        </div>
        <div className="grid grid-cols-4 space-x-5">
          <div className="col-span-3">
            <div>
              <h1 className="font-bold ml-4">Station Activity</h1>
              <Separator className="my-5 bg-border/50" />
              <div className="mt-3 flex flex-col gap-4">
                <div className="border-2 text-sm p-4 rounded-lg">
                  <div className="flex justify-between">
                    <p>Cable unplugged</p>
                    <p className="text-muted/70 text-xs self-end">15:29, 10th July 2023</p>
                  </div>
                </div>
                <div className="border-2 text-sm p-4 rounded-lg">
                  <div className="flex justify-between">
                    <p>EV fully charged</p>
                    <p className="text-muted/70 text-xs self-end">15:30, 10th July 2023</p>
                  </div>
                </div>
                <div className="border-2 text-sm p-4 rounded-lg">
                  <div className="flex justify-between">
                    <p>Started Charging the EV</p>
                    <p className="text-muted/70 text-xs self-end">15:31, 10th July 2023</p>
                  </div>
                </div>
                <div className="border-2 text-sm p-4 rounded-lg">
                  <div className="flex justify-between">
                    <p>Transaction started</p>
                    <p className="text-muted/70 text-xs self-end">15:31, 10th July 2023</p>
                  </div>
                </div>
                <div className="border-2 text-sm p-4 rounded-lg">
                  <div className="flex justify-between">
                    <p>Cable plugged in</p>
                    <p className="text-muted/70 text-xs self-end">15:32, 10th July 2023</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="col-span-1">
            <div className="flex flex-col gap-8">
              <div>
                <h1 className="font-bold ml-4">Station Management</h1>
                <Separator className="my-5 bg-border/50" />
                <div className="mt-3 flex flex-col gap-4">
                  <div>
                    <p className="text-xs">Location</p>
                    <h3 className="text-sm font-semibold mt-1">Kejawen Putih Tambak 20</h3>
                    <p className="text-xs text-muted/80">Pakuwon City, Surabaya</p>
                  </div>
                  <div>
                    <p className="text-xs">Tariff</p>
                    <h3 className="text-sm font-semibold mt-1">Basic Tariff</h3>
                    <p className="text-xs text-muted/80">Rp. 3.000 per kWh</p>
                  </div>
                  <div>
                    <p className="text-xs">Station Type</p>
                    <h3 className="text-sm font-semibold mt-1">Public Charger</h3>
                  </div>
                  <div>
                    <p className="text-xs">Location</p>
                    <h3 className="text-sm font-semibold mt-1">Drivers allow list</h3>
                    <p className="text-xs text-muted/80">8 Drivers</p>
                  </div>
                </div>
              </div>
              <div>
                <h1 className="font-bold ml-4">Station Details</h1>
                <Separator className="my-5 bg-border/50" />
                <div className="grid grid-cols-2 gap-4">
                  <div className="col-span-1">
                    <p className="text-xs font-semibold">Manufacturer</p>
                    <h3 className="text-sm">ABB</h3>
                  </div>
                  <div>
                    <p className="text-xs font-semibold">Firmware</p>
                    <h3 className="text-sm">1.30.1</h3>
                  </div>
                  <div>
                    <p className="text-xs font-semibold">Model</p>
                    <h3 className="text-sm">Type AAA - 123</h3>
                  </div>
                  <div>
                    <p className="text-xs font-semibold">Serial number </p>
                    <h3 className="text-sm">KU2318AX</h3>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </main>
  );
};

export default Home;
