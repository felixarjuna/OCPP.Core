import CreateStationForm from "@/app/components/stations/forms/create-station-form";
import { Separator } from "@/components/ui/separator";

export const Home = () => {
  return (
    <main className="col-span-4 p-5 space-y-5">
      <div>
        <div className="flex justify-between items-center">
          <h1 className="font-bold ml-4">Add Charge Station</h1>
        </div>
        <Separator className="my-5 bg-border/50" />
      </div>
      <div>
        <CreateStationForm />
      </div>
    </main>
  );
};

export default Home;
