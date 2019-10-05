class HospitalBox extends React.Component {
    render() {
        return (
            <div className="hospitalBox">
                <h1>Comments</h1>
                <h2 className="hospitalName">"Hopitals"{this.props.name}</h2>
                {this.props.children}
                <HospitalList data={this.props.data} />
                
            </div>
        );
    }
}

class HospitalList extends React.Component {
    render() {

        const hospitalNodes = this.props.data.map(hospital => (
            <Hospital name={hospital.name} key={hospital.Id}>
                {hospital.name}
            </Hospital>
        ));
        return <div className="hospitalList">{hospitalNodes}</div>;

    }
}

class Hospital extends React.Component {
    rawMarkup() {
        const md = new Remarkable();
        const rawMarkup = md.render(this.props.children.toString());
        return { __html: rawMarkup };
    }
    render() {
        return (
            <div className="hospital">
                <h2 className="hospitalName">{this.props.name}</h2>
                <span dangerouslySetInnerHTML={this.rawMarkup()} />
            </div>
        );
    }
}
ReactDOM.render(
    <HospitalBox url="/hospitals" />,
    document.getElementById('content'),
);

