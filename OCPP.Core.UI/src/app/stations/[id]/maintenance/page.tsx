import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { ArrowDownCircle, ChevronsUpDown, MoreVertical, Sliders, Zap } from "lucide-react";
import Image from "next/image";

export const Home = () => {
  return (
    <section className="grid grid-cols-4 space-x-5 p-4">
      <div className="col-span-1 space-y-5 px-4">
        <div>
          <h1 className="font-bold text-sm">Components</h1>
          <p className="text-xs text-muted/80">2 Connectors</p>
        </div>
        <Button variant={"secondary"} className="flex justify-between h-10 w-full">
          <div className="flex items-center gap-2">
            <Zap className="w-4 h-4" />
            <h3>Station</h3>
          </div>
          <div>
            <span className="relative flex h-[10px] w-[10px]">
              <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-green-400 opacity-75"></span>
              <span className="relative inline-flex rounded-full h-[10px] w-[10px] bg-green-500"></span>
            </span>
          </div>
        </Button>

        <div>
          <h3 className="text-sm font-bold">EVSE One</h3>
          <Button variant={"ghost"} className="flex mt-3 w-full h-10 justify-between">
            <div className="flex items-center gap-2">
              <div className="w-4">
                <Image src={"/connector.svg"} alt={""} width={400} height={400} />
              </div>
              <div className="flex flex-col justify-center items-start">
                <h3 className="text-sm">Connector One</h3>
                <p className="text-xs text-muted/80">Type 2</p>
              </div>
            </div>
            <div>
              <span className="relative flex h-[10px] w-[10px]">
                <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-green-400 opacity-75"></span>
                <span className="relative inline-flex rounded-full h-[10px] w-[10px] bg-green-500"></span>
              </span>
            </div>
          </Button>
        </div>

        <div>
          <h3 className="text-sm font-bold">EVSE Two</h3>
          <Button variant={"ghost"} className="flex mt-3 w-full h-10 justify-between">
            <div className="flex items-center gap-2">
              <div className="w-4">
                <Image src={"/connector.svg"} alt={""} width={400} height={400} />
              </div>
              <div className="flex flex-col justify-center items-start">
                <h3 className="text-sm">Connector One</h3>
                <p className="text-xs text-muted/80">Type 2</p>
              </div>
            </div>
            <div>
              <span className="relative flex h-[10px] w-[10px]">
                <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-green-400 opacity-75"></span>
                <span className="relative inline-flex rounded-full h-[10px] w-[10px] bg-green-500"></span>
              </span>
            </div>
          </Button>
        </div>
      </div>
      <div className="col-span-1 space-y-5">
        <div className="flex items-center justify-center flex-col gap-3">
          <div className="rounded-full w-20 h-20 bg-slate-600"></div>
          <h2>Alfen Double</h2>
        </div>
        <div className="flex gap-2 text-sm items-center justify-center">
          <Button variant={"secondary"} className="w-36">
            Message Logs
          </Button>
          <Button variant={"secondary"} className="w-36">
            Device Model
          </Button>
        </div>
        <div className="space-y-3">
          <h2 className="text-sm font-bold">Technical Details</h2>
          <div>
            <p className="text-xs">Station ID</p>
            <h3 className="text-sm">5377738888293</h3>
          </div>
          <div>
            <p className="text-xs">Firmware</p>
            <h3 className="text-sm">1.31.2</h3>
          </div>
          <div>
            <p className="text-xs">Charger Protocol</p>
            <h3 className="text-sm">OCPP 2.0.1</h3>
          </div>
          <div>
            <p className="text-xs">Security Profile</p>
            <h3 className="text-sm">Profile 3</h3>
          </div>
        </div>
      </div>
      <div className="col-span-2 space-y-5">
        <div className="flex justify-between">
          <div>
            <h3 className="text-bold font-bold text-sm">Message Logs</h3>
            <p className="text-xs text-muted/80">Active now</p>
          </div>
          <div className="flex gap-2">
            <Button variant={"secondary"}>
              <MoreVertical className="w-4 h-4" />
            </Button>
            <Button variant={"secondary"}>
              <ChevronsUpDown className="w-4 h-4" />
            </Button>
            <Button variant={"secondary"}>
              <Sliders className="w-4 h-4" />
            </Button>
          </div>
        </div>
        <div className="flex gap-3">
          <Avatar>
            <AvatarImage className="" />
            <AvatarFallback></AvatarFallback>
          </Avatar>
          <div className="px-4 py-3 w-full bg-slate-700/50 rounded-lg flex justify-between items-center">
            <div className="flex flex-col">
              <p className="text-xs text-muted/80">From Ploeg at 14:47:15</p>
              <h3 className="text-sm">TransactionEvent Call</h3>
            </div>
            <div>
              <ArrowDownCircle className="w-4 h-4" />
            </div>
          </div>
        </div>

        <div className="flex gap-3">
          <Avatar>
            <AvatarImage className="" />
            <AvatarFallback></AvatarFallback>
          </Avatar>
          <div className="px-4 py-3 w-full bg-slate-700/50 rounded-lg flex justify-between items-center">
            <div className="flex flex-col">
              <p className="text-xs text-muted/80">From Ploeg at 14:47:12</p>
              <h3 className="text-sm">Authorize CallResult</h3>
            </div>
            <div>
              <ArrowDownCircle className="w-4 h-4" />
            </div>
          </div>
        </div>

        <div className="flex gap-3">
          <Avatar>
            <AvatarImage className="" />
            <AvatarFallback></AvatarFallback>
          </Avatar>
          <div className="px-4 py-3 w-full bg-slate-700/50 rounded-lg flex justify-between items-center">
            <div className="flex flex-col">
              <p className="text-xs text-muted/80">From Ploeg at 14:47:11</p>
              <h3 className="text-sm">Authorize Call</h3>
            </div>
            <div>
              <ArrowDownCircle className="w-4 h-4" />
            </div>
          </div>
        </div>

        <div className="flex gap-3">
          <Avatar>
            <AvatarImage className="" />
            <AvatarFallback></AvatarFallback>
          </Avatar>
          <div className="px-4 py-3 w-full bg-slate-700/50 rounded-lg flex justify-between items-center">
            <div className="flex flex-col">
              <p className="text-xs text-muted/80">From Ploeg at 14:47:10</p>
              <h3 className="text-sm">SessionSetup Response</h3>
            </div>
            <div>
              <ArrowDownCircle className="w-4 h-4" />
            </div>
          </div>
        </div>

        <div className="flex gap-3">
          <Avatar>
            <AvatarImage className="" />
            <AvatarFallback></AvatarFallback>
          </Avatar>
          <div className="px-4 py-3 w-full bg-slate-700/50 rounded-lg flex justify-between items-center">
            <div className="flex flex-col">
              <p className="text-xs text-muted/80">From Ploeg at 14:47:08</p>
              <h3 className="text-sm">SessionSetup Request</h3>
            </div>
            <div>
              <ArrowDownCircle className="w-4 h-4" />
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Home;
