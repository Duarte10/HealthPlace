import * as React from 'react';

type Visitor = {
    name: string,
    mobile: string,
    email: string
}

class VisitorsPage extends React.Component<{ history: any }, {}> {
    render() {
        return <>
            <button className="btn btn-primary" style={{ marginBottom: '15px' }} onClick={() => this.props.history.push('/visitors/new')}>New</button>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Name</th>
                        <th scope="col">Mobile</th>
                        <th scope="col">Email</th>
                        <th scope="col"> </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>João</td>
                        <td>+351 912 021 421</td>
                        <td>joao@gmail.com</td>
                        <td>
                            <button className="btn btn-secondary btn-sm" style={{ marginRight: '5px' }}>Edit</button>
                            <button className="btn btn-secondary btn-sm">Delete</button>
                        </td>
                    </tr>
                    <tr>
                        <td>Joana</td>
                        <td>+351 934 021 421</td>
                        <td>joana@gmail.com</td>
                        <td>
                            <button className="btn btn-secondary btn-sm" style={{ marginRight: '5px' }}>Edit</button>
                            <button className="btn btn-secondary btn-sm">Delete</button>
                        </td>
                    </tr>
                    <tr>
                        <td>Pedro</td>
                        <td>+351 925 021 421</td>
                        <td>miguel@gmail.com</td>
                        <td>
                            <button className="btn btn-secondary btn-sm" style={{ marginRight: '5px' }}>Edit</button>
                            <button className="btn btn-secondary btn-sm">Delete</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </>
    }
}

export default VisitorsPage;
