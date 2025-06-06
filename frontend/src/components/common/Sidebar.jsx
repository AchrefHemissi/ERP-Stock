import {
	BarChart2,
	Receipt,
	Clock,
	Menu,
	Settings,
	ShoppingBag,
	ShoppingCart,
	Package,
	TrendingUp,
	AlertTriangle,
	ArrowRightLeft,
	Warehouse
} from "lucide-react";
import { useState } from "react";
import { AnimatePresence, motion } from "framer-motion";
import { Link } from "react-router-dom";
import { getUserRole } from "../../utility/storage";



const SIDEBAR_ITEMS = [
	{
		name: "Overview",
		icon: BarChart2,
		color: "#6366f1",
		href: "/",
	},
	{ name: "Products", icon: ShoppingBag, color: "#8B5CF6", href: "/products" },
	{name: "Products Blocks", icon: Package, color: "#8B5CF6", href: "/productsBlocks" },
	{name: "Warehouse", icon: Warehouse, color: "#8B5CF6", href: "/warehouse" },
	{ name: "Low Stock", icon: AlertTriangle, color: "#F59E0B", href: "/lowstock" },
	//{ name: "Expiration", icon: Clock, color: "red", href: "/delay" },
	{ name: "Purchases", icon: Receipt, color: "#F59E0B", href: "/purchases" },
	{ name: "Orders", icon: ShoppingCart, color: "#F59E0B", href: "/orders" },
	{ name: "Transfers", icon: ArrowRightLeft, color: "#6EE7B7", href: "/transfers" },
	// { name: "Analytics", icon: TrendingUp, color: "#3B82F6", href: "/analytics" },
	{ name: "Settings", icon: Settings, color: "#6EE7B7", href: "/settings" },
	
];

const Sidebar = () => {

	const role = getUserRole();
	console.log("role mt3i: ", role);

	const [isSidebarOpen, setIsSidebarOpen] = useState(true);

	return (
		<motion.div
			className={`relative z-10 transition-all duration-150 ease-in-out flex-shrink-0 ${
				isSidebarOpen ? "w-64" : "w-20"
			}`}
			animate={{ width: isSidebarOpen ? 256 : 80 }}
		>
			<div className='h-full bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-0 backdrop-blur-md p-4 flex flex-col    rounded-br-[80px]'>
				<motion.button
					whileHover={{ scale: 1.1 }}
					whileTap={{ scale: 0.9 }}
					onClick={() => setIsSidebarOpen(!isSidebarOpen)}
					className='p-2 rounded-full hover:bg-gradient-to-br from-cyan-200 to-cyan-300  transition-colors max-w-fit'
				>
					<Menu size={24} />
				</motion.button>

				<nav className='mt-8 flex-grow'>
					{SIDEBAR_ITEMS.map((item) => (
						<Link key={item.href} to={item.href}>
							<motion.div className='flex items-center p-4 text-sm font-medium rounded-lg hover:bg-gradient-to-br from-cyan-200 to-cyan-300 hover:text-blue-950  mb-2'>
								<item.icon size={20} style={{ color: item.color, minWidth: "20px" }} />
								<AnimatePresence>
									{isSidebarOpen && (
										<motion.span
											className='ml-4 whitespace-nowrap'
											initial={{ opacity: 0, y: 0 }}
			animate={{ opacity: 1, y: 0 }}
			transition={{ duration: 0.2 }}
										>
											{item.name}
										</motion.span>
									)}
								</AnimatePresence>
							</motion.div>
						</Link>
					))}
				</nav>
				{role==="Admin" && <div className="flex justify-center items-center">
				<Link className="flex  mb-12 justify-center items-center min-w-full" key="/add" to="/add" >
				
				<button className=" mb-[40px] ml-auto max-h-12  min-w-[60px] group cursor-pointer outline-none " title="Add New">
					{isSidebarOpen && <span className=" text-lg w-96 ">Add Stock Manager</span>}
				<svg className="ml-auto mr-auto stroke-sky-500 fill-none hover:rotate-90 duration-300 group-hover:fill-white group-active:stroke-sky-200 group-active:fill-sky-600 group-active:duration-0 duration-300" viewBox="0 0 24 24" height="60px" width="60px" xmlns="http://www.w3.org/2000/svg">
					<path strokeWidth="1.5" d="M12 22C17.5 22 22 17.5 22 12C22 6.5 17.5 2 12 2C6.5 2 2 6.5 2 12C2 17.5 6.5 22 12 22Z" />
					<path strokeWidth="1.5" d="M8 12H16" />
					<path strokeWidth="1.5" d="M12 16V8" />
				</svg>
				</button>
				</Link>
				</div>}
				
				
	
			</div>
		</motion.div>
	);
};
export default Sidebar;
