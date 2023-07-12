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
import { MoreHorizontal, Pen, Trash } from "lucide-react";
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
        <h1 className="font-bold">{row.original.name}</h1>
        <p className="text-muted/60">{row.original.chargeStationId}</p>
      </div>
    ),
  },
  {
    accessorKey: "location",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Location" className="text-white" />
    ),
  },
  {
    accessorKey: "stationStatus",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Station Status" className="text-white" />
    ),
  },
  {
    accessorKey: "evseStatus",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="EVSE Status" className="text-white" />
    ),
  },
  {
    accessorKey: "energy",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Energy" className="text-white" />
    ),
  },
  {
    accessorKey: "money",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Money" className="text-white" />
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
              <Link href={`/admin/products/update/${row.original.chargeStationId}`}>Edit</Link>
            </DropdownMenuItem>
            <DropdownMenuItem
              onClick={() => deleteChargeStation(row.original.chargeStationId)}
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
