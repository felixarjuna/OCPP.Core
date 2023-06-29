"use client";

import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Separator } from "@/components/ui/separator";
import { columns } from "../components/stations/columns";
import DataTable from "../components/stations/data-table";
import { useChargePoints } from "../hooks/useChargePoints";

export default function Home() {
  const { stations } = useChargePoints();

  return (
    <main className="col-span-4 p-5 space-y-5">
      <div>
        <h1 className="font-bold ml-4">All stations</h1>
        <Separator className="my-5 bg-border/50" />
      </div>
      <div className="w-36">
        <Select>
          <SelectTrigger>
            <SelectValue placeholder="Status" />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="offline">Offline</SelectItem>
            <SelectItem value="charging">Charging</SelectItem>
            <SelectItem value="available">Available</SelectItem>
          </SelectContent>
        </Select>
      </div>
      <div>
        <DataTable data={stations} columns={columns} />
      </div>
    </main>
  );
}
