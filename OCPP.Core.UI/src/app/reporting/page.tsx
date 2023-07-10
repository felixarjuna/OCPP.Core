export const Home = () => {
  return (
    <section className="grid grid-cols-6 p-5 gap-4">
      <div className="col-span-2">
        <div className="bg-slate-700/50 p-5 h-64 rounded-lg">
          <h3 className="text-sm">Authorization Type</h3>
          <p className="text-2xl mt-1">24,787</p>
          <p className="text-xs text-muted/80">Authorized charge sessions</p>
        </div>
      </div>
      <div className="col-span-4">
        <div className="bg-slate-700/50 p-5 h-64 rounded-lg">
          <h3 className="text-sm">Money generated</h3>
          <p className="text-2xl mt-1">Rp. 523.334,89</p>
          <p className="text-xs text-muted/80">Converted to IDR</p>
        </div>
      </div>
      <div className="col-span-4">
        <div className="bg-slate-700/50 p-5 h-64 rounded-lg">
          <h3 className="text-sm">Charge Types</h3>
          <p className="text-xs text-muted/80">Comparing AC and DC chargers usage.</p>
        </div>
      </div>
      <div className="col-span-2">
        <div className="bg-slate-700/50 p-5 h-64 rounded-lg">
          <h3 className="text-sm">Energy Delivered</h3>
          <p className="text-2xl mt-1">
            51,987<span className="text-sm">kWh</span>
          </p>
          <p className="text-xs text-muted/80">Over 6 months</p>
        </div>
      </div>
    </section>
  );
};

export default Home;
