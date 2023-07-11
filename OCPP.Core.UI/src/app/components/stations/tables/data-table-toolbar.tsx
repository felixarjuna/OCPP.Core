import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { type Table } from "@tanstack/react-table";
import { X } from "lucide-react";
import { DataTableFacetedFilter } from "./data-table-faceted-filter";
import { DataTableViewOptions } from "./data-table-view-options";

interface DataTableToolbarProps<TData> {
  table: Table<TData>;
}

export function DataTableToolbar<TData>({ table }: DataTableToolbarProps<TData>) {
  const isFiltered =
    table.getPreFilteredRowModel().rows.length > table.getFilteredRowModel().rows.length;

  return (
    <div className="flex items-center justify-between">
      <div className="flex flex-1 items-center space-x-2">
        <Input
          placeholder="Filter products..."
          className="h-10 max-w-xs lg:max-w-sm bg-transparent outline-none focus-visible:outline-none focus-visible:ring-0 placeholder:text-white/50"
          value={(table.getColumn("station")?.getFilterValue() as string) ?? ""}
          onChange={(e) => table.getColumn("station")?.setFilterValue(e.target.value)}
        />
        {table.getColumn("station") && (
          <DataTableFacetedFilter
            column={table.getColumn("station")}
            title="Station"
            options={[]}
          />
        )}

        {isFiltered && (
          <Button
            variant="ghost"
            onClick={() => table.resetColumnFilters()}
            className="h-10 px-2 lg:px-3"
          >
            Reset
            <X className="ml-2 h-4 w-4" />
          </Button>
        )}
      </div>
      <DataTableViewOptions table={table} />
    </div>
  );
}
