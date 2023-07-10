import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { PlusCircle } from "lucide-react";
import Link from "next/link";

export const StationLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <main className="col-span-4 p-5 space-y-5">
      <div>
        <div className="flex justify-between items-center">
          <h1 className="font-bold ml-4">All stations</h1>
          <div className="flex gap-12 text-sm">
            <Link href={""}>Stations</Link>
            <Link href={""}>Pending</Link>
          </div>
          <Button variant={"secondary"} className="flex gap-2 h-10">
            <PlusCircle className="w-4 h-4" />
            <p>Add a station</p>
          </Button>
        </div>
        <Separator className="my-5 bg-border/50" />
      </div>
      {children}
    </main>
  );
};

export default StationLayout;
