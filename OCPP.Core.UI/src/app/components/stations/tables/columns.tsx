import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { deleteChargeStation } from "@/lib/api";
import { ChargeStation } from "@/lib/contracts";
import { type ColumnDef } from "@tanstack/react-table";
import { AlertCircle, CheckCircle2, MoreHorizontal, Pen, Trash, Zap } from "lucide-react";
import Link from "next/link";
import { DataTableColumnHeader } from "./data-table-column-header";

export const columns: ColumnDef<ChargeStation>[] = [
  {
    accessorKey: "station",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Station Name & Id" className="text-white" />
    ),
    cell: ({ row }) => (
      <div className="">
        <h1 className="font-bold">{row.original.stationName}</h1>
        <p className="text-muted/60">{row.original.stationId}</p>
      </div>
    ),
  },
  {
    accessorKey: "location",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Location" className="text-white" />
    ),
    cell: ({ row }) => (
      <div>
        <h1 className="font-bold">{row.original.street}</h1>
        <p className="text-muted/60">{row.original.city}</p>
      </div>
    ),
  },
  {
    accessorKey: "stationStatus",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Station Status" className="text-white" />
    ),
    cell: ({ row }) => {
      if (row.original.online)
        return (
          <div className="flex items-center gap-2">
            <CheckCircle2 className="w-5 h-5 text-green-400" />
            <p>Operational</p>
          </div>
        );
      return (
        <div className="flex items-center gap-2 text-muted">
          <AlertCircle className="w-5 h-5 text-yellow-400" />
          <p>Offline</p>
        </div>
      );
    },
  },
  {
    accessorKey: "evseStatus",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="EVSE Status" className="text-white" />
    ),
    cell: ({ row }) => {
      {
        return row.original.connectors?.map((connector) => {
          <Zap />;
        });
      }
    },
  },
  {
    accessorKey: "energy",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Energy" className="text-white" />
    ),
    cell: ({ row }) => <p className="font-bold">{row.original.energy} kWh</p>,
  },
  {
    accessorKey: "money",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Money & Sessions" className="text-white" />
    ),
    cell: ({ row }) => (
      <div>
        <p className="font-bold">Rp. {row.original.money}</p>
        <p className="text-muted/60">{row.original.transactions}</p>
      </div>
    ),
  },
  {
    accessorKey: "model",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Model" className="text-white" />
    ),
    cell: ({ row }) => (
      <div>
        <h1 className="font-bold">
          {row.original.model} {row.original.serialNumber}
        </h1>
        <p className="text-muted/60">{row.original.vendorName}</p>
      </div>
    ),
  },
  {
    id: "actions",
    cell: ({ row }) => {
      return (
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="ghost" className="h-8 w-8 p-0">
              <span className="sr-only">Open menu</span>
              <MoreHorizontal className="h-4 w-4" />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align="end" className="bg-black text-primary-foreground">
            <DropdownMenuLabel>Actions</DropdownMenuLabel>
            <DropdownMenuItem onClick={() => console.log("edit item")} className="group">
              <Pen className="text-primary-foreground/70 mr-2 h-3.5 w-3.5 group-hover:text-black/70" />
              <Link href={`/admin/products/update/${row.original.stationId}`}>Edit</Link>
            </DropdownMenuItem>
            <DropdownMenuItem
              onClick={() => deleteChargeStation(row.original.stationId)}
              className="group"
            >
              <Trash className="text-primary-foreground/70 mr-2 h-3.5 w-3.5 group-hover:text-black/70" />
              Delete
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      );
    },
  },
];
