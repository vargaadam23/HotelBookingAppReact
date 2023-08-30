import React, { Component } from 'react';
import { UncontrolledPopover, PopoverHeader, PopoverBody, Button } from 'reactstrap';

export class FacilityBadge extends Component {
    static displayName = FacilityBadge.name;

    renderAsPopover() {
        const {
            facility: {
                name = '',
                description = ''
            } = {},
            isSimple = true
        } = this.props;

        if (isSimple) {
            return null;
        }

        return (
            <>
                <Button
                    id="PopoverLegacy"
                    type="button"
                >
                    {name}
                </Button>
                <UncontrolledPopover
                    target="PopoverLegacy"
                    trigger="legacy"
                >
                    <PopoverHeader>
                        {name}
                    </PopoverHeader>
                    <PopoverBody>
                        {description}
                    </PopoverBody>
                </UncontrolledPopover>
            </>

        )
    }

    renderSimple() {
        const {
            facility: {
                name = ''
            } = {},
            isSimple = true
        } = this.props;

        if (!isSimple) {
            return null;
        }

        return (
            <span className="badge badge-dark">{name}</span>
        );
    }

    render() {
        const {
            isSimple = true
        } = this.props;

        if (isSimple) {
            return this.renderSimple();
        }

        return this.renderAsPopover();
    }
}
