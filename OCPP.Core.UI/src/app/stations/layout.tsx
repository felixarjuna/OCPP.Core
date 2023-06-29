import { Sidebar } from "../components/sidebar";

export default function DashboardLayout({ children }: { children: React.ReactNode }) {
  return (
    <main className="min-h-screen">
      <div className="p-10">
        <div className="min-h-[calc(100vh-80px)] border rounded-xl grid lg:grid-cols-5 p-8">
          <Sidebar />
          {children}
        </div>
      </div>
    </main>
  );
}
