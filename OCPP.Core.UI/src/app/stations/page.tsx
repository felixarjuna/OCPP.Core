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
    </section>
  );
}
