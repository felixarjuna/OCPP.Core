"use client";

import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

import { useChargePoints } from "@/hooks/useChargePoints";
import { columns } from "../components/stations/columns";
import DataTable from "../components/stations/data-table";

export default function Home() {
  const { stations } = useChargePoints();

  return (
    <section className="space-y-5">
      <div className="flex gap-3">
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
        <div className="w-36">
          <Select>
            <SelectTrigger>
              <SelectValue placeholder="OCPP" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="2.0">OCPP 2.0.1</SelectItem>
              <SelectItem value="1.6">OCPP 1.6</SelectItem>
              <SelectItem value="1.5">OCPP 1.5</SelectItem>
            </SelectContent>
          </Select>
        </div>
      </div>
      <div className="w-full h-80 bg-slate-600 rounded-lg flex justify-center items-center">
        Map
      </div>
      <div>
        <DataTable data={stations} columns={columns} />
      </div>
    </section>
  );
}
