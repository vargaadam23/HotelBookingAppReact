import { Component } from 'react';
import { Spinner } from 'reactstrap';
export class Loader extends Component {
    static displayName = Loader.name;

    render() {
        const {
            isLoading
        } = this.props;

        if (!isLoading) {
            return null;
        }

        return (
            <div className="container loader-wrapper">
                <Spinner>
                    ...Loading
                </Spinner>
            </div>
        );
    }
}
