import { CompanySearch } from '../../FinModAPI/CompanyModels';
import Card from '../Card/Card'
import './CardList.css'
import { v4 as uuidv4 } from 'uuid';

import React, { SyntheticEvent } from 'react'

interface Props {
  searchResult: CompanySearch[] | undefined;
  onPortfolioAdd: (e: SyntheticEvent) => void;
}

const CardList = (props: Props) => {
  return (
    <div className='relative flex flex-col items-center max-w-5xl mx-auto'>
      {(props.searchResult !== undefined && props.searchResult?.length > 0)
        ? props.searchResult.map(result => {
          return <Card id={result.symbol} key={uuidv4()} company={result} onPortfolioAdd={props.onPortfolioAdd} />
        })
        :
        <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
          No results!
        </p>}
    </div>
  )
}

export default CardList