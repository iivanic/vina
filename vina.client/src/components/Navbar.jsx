
import { Nav, NavLink, NavMenu } from "./NavbarElements";

const Navbar = () => {
    return (
        <>
            <Nav>
                <NavMenu>
                    <NavLink to="/" activeStyle>
                        Home
                    </NavLink>
                    <NavLink to="/account" activeStyle>
                        Account
                    </NavLink>
                    <NavLink to="/signin" activeStyle>
                        Sign In
                    </NavLink>
                </NavMenu>
            </Nav>
        </>
    );
};

export default Navbar;