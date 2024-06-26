import { Link } from 'react-router-dom'
import './Sidebar.css'
import {FaHome, FaMoneyBill, FaBalanceScaleLeft} from 'react-icons/fa'
import {SiCashapp} from 'react-icons/si'

import React from 'react'

interface Props {}

const Sidebar = (props: Props) => {
  return (
    <nav className="block py-4 px-6 top-0 bottom-0 w-64 bg-white shadow-xl left-0 absolute flex-row flex-nowrap md:z-10 z-9999 transition-all duration-300 ease-in-out transform md:translate-x-0 -translate-x-full">
        <button className="md:hidden flex items-center justify-center cursor-pointer text-blueGray-700 w-6 h-10 border-l-0 border-r border-t border-b border-blueGray-100 text-xl leading-none bg-white rounded-r border border-solid border-transparent absolute top-1/2 -right-24-px focus:outline-none z-9998">
          <i className="fas fa-ellipsis-v"></i>
        </button>

        <div className="flex-col min-h-full px-0 flex flex-wrap items-center justify-between w-full mx-auto overflow-y-auto overflow-x-hidden">
          <div className="flex bg-white flex-col opacity-100 relative mt-4 overflow-y-auto overflow-x-hidden h-auto z-40 items-center flex-1 rounded w-full">
            <div className="md:flex-col md:min-w-full flex flex-col list-none">
              <Link to="company-profile" className="text-center lg:flex-row flex text-blueGray-500 text-xs uppercase font-bold pt-1 pb-4 no-underline">
                <FaHome className='mt-0.5'/>
                <h6 className="ml-3">
                    Company Profile
                </h6>
              </Link>

              <Link to="income-statement" className="text-center lg:flex-row flex text-blueGray-500 text-xs uppercase font-bold pt-1 pb-4 no-underline">
                <FaMoneyBill className='mt-0.5' />
                <h6 className="ml-3">
                    Income Statement
                </h6>
              </Link>

              <Link to="balance-sheet" className="text-center lg:flex-row flex text-blueGray-500 text-xs uppercase font-bold pt-1 pb-4 no-underline">
                <FaBalanceScaleLeft className='mt-0.5' />
                <h6 className="ml-3">
                    Balance Sheet
                </h6>
              </Link>

              <Link to="cashflow-statement" className="text-center lg:flex-row flex text-blueGray-500 text-xs uppercase font-bold pt-1 pb-4 no-underline">
                <SiCashapp className='mt-0.5' />
                <h6 className="ml-3">
                    Cashflow Statement
                </h6>
              </Link>
            </div>
          </div>
        </div>
      </nav>
  )
}

export default Sidebar