import React from 'react';

export default class Tenant extends React.PureComponent {
    componentDidMount() {
        // This method is called when the component is first added to the document
        this.storeTenantId();
    }

    componentDidUpdate(prevProps) {
        // This method is called when the route parameters change
        if (this.props.match.params.tenantId !== prevProps.match.params.tenantId) {
            this.storeTenantId();
        }
    }

    storeTenantId() {
        const tenantId = this.props.match.params.tenantId;
        localStorage.setItem('TenantId', tenantId);
    }

    render() {
        const tenantId = this.props.match.params.tenantId;
        return (
            <div>
                <p>Tenant {tenantId}</p>
            </div>
        )
    }
}