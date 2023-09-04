import React, { Component } from 'react';
import authService from '../components/api-authorization/AuthorizeService';
import {Spinner } from 'reactstrap'

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props)
        this.state = {
            loading: false,
            token: ''
        }
    }

    componentDidMount() {
        this.getCurrentToken();
    }
    
   getCurrentToken() {
        this.setState({loading: true})
        if (authService.isAuthenticated()) {
            const user = authService.getAccessToken().then((res) => this.setState({ token: res })).finally(() => this.setState({ loading: false }))
            console.log(user)
            
           
        }
        
    }

    renderToken() {
        const { loading, token } = this.state

        if (loading) {
            return <Spinner/>
        }
        console.log(token)
        return (
            <div className="container">
                <span className="">
                    {token}
                </span>
            </div>
        )
    }

    render() {
        const { loading, token } = this.state;
    return (
      <div>
        <h1>Hello, world!</h1>
            <p>Your authentication token for postman is <b>{this.renderToken()}</b></p>
      </div>
    );
  }
}
