import { useParams } from 'react-router'
import './CompanyPage.css'

import React, { useEffect, useState } from 'react'
import { CompanyProfile } from '../../FinModAPI/CompanyModels';
import { getCompanyProfile } from '../../FinModAPI/FinModApi';
import Sidebar from '../../Components/Sidebar/Sidebar';
import CompanyDashboard from '../../Components/Company/CompanyDashboard/CompanyDashboard';
import Tile from '../../Components/Tile/Tile';
import Spinner from '../../Components/Spinner/Spinner';

interface Props { }

const CompanyPage = (props: Props) => {
  let { ticker } = useParams();
  const [company, setCompany] = useState<CompanyProfile>();

  useEffect(() => {
    const getProfileInit = async () => {
      const result = await getCompanyProfile(ticker!);
      setCompany(result?.data[0]);
    }
    getProfileInit();
  }, []);

  return (
    <>
      {company ? (
        <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
          <Sidebar />
          <CompanyDashboard ticker={ticker!}>
            <Tile title='Name' subTitle={company.companyName} />
            <Tile title='Sector' subTitle={company.sector} />
            <Tile title='Price' subTitle={'$' + company.price.toString()} />
            <Tile title='DCF' subTitle={'$' + company.dcf.toPrecision(4).toString()} />
            <p className='bg-white shadow rounded text-medium text-gray-900 p-3 mt-1 m-4'>
              {company.description}
            </p>
          </CompanyDashboard>
        </div>
      ) : <Spinner />
      }
    </>
  )
}

export default CompanyPage