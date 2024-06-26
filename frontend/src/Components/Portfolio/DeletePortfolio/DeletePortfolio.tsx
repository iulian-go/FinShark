import './DeletePortfolio.css'

import React, { SyntheticEvent } from 'react'

interface Props {
  ticker: string;
  onPortfolioDelete: (e: SyntheticEvent) => void;
}

const DeletePortfolio = (props: Props) => {
  return (
    <div>
      <form onSubmit={props.onPortfolioDelete}>
        <input readOnly={true} hidden={true} value={props.ticker} />
        <button className="block w-full py-3 text-white duration-200 border-2 rounded-lg bg-red-500 hover:text-red-500 hover:bg-white border-red-500">
          X
        </button>
      </form>
    </div>
  )
}

export default DeletePortfolio