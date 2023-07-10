export const Home = () => {
  return (
    <section className="grid grid-cols-3 gap-4">
      <div className="col-span-1">
        <div className="p-5 rounded-lg bg-slate-800/50">
          <h3 className="text-sm text-muted/80">Money generated</h3>
          <p className="text-2xl mt-1">Rp. 580.231</p>
        </div>
      </div>
      <div className="col-span-1">
        <div className="p-5 rounded-lg bg-slate-800/50">
          <h3 className="text-sm text-muted/80">Charge Sessions</h3>
          <p className="text-2xl mt-1">2.130</p>
        </div>
      </div>
      <div className="col-span-1">
        <div className="p-5 rounded-lg bg-slate-800/50">
          <h3 className="text-sm text-muted/80">Energy Delivered</h3>
          <p className="text-2xl mt-1">
            82<span className="text-lg">kWh</span>
          </p>
        </div>
      </div>
    </section>
  );
};

export default Home;
