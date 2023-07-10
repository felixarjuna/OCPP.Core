export const Home = () => {
  return (
    <section className="grid grid-cols-8 p-5 gap-4">
      <div className="col-span-3">
        <div className="bg-slate-700/50 p-5 h-64 rounded-lg">
          <h3 className="font-semibold">Tariffs Details</h3>

          <div className="mt-2 flex gap-3 justify-between">
            <div className="flex flex-col gap-1">
              <p className="text-xs text-muted/80">Currency</p>
              <p className="text-sm">Rupiah (Rp.)</p>
            </div>
            <div className="flex flex-col gap-1">
              <p className="text-xs text-muted/80">Session Fee</p>
              <p className="text-sm">Rp. 30.000</p>
            </div>
            <div className="flex flex-col gap-1">
              <p className="text-xs text-muted/80">Tariff Type</p>
              <p className="text-sm">Variable</p>
            </div>
          </div>
        </div>
      </div>
      <div className="col-span-5">
        <div className="bg-slate-700/50 p-5 h-64 rounded-lg">
          <h3 className="font-semibold">Tariffs Performance</h3>

          <div className="mt-2 flex gap-3 justify-between">
            <div className="flex flex-col gap-1">
              <p className="text-xs text-muted/80">Money Generated</p>
              <p className="text-2xl mt-1">Rp. 523.334,89</p>
            </div>
            <div className="flex flex-col gap-1">
              <p className="text-xs text-muted/80">Stations Using Tariff</p>
              <p className="text-2xl mt-1">23</p>
            </div>
            <div className="flex flex-col gap-1">
              <p className="text-xs text-muted/80">Tariff Sessions</p>
              <p className="text-2xl mt-1">421</p>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Home;
