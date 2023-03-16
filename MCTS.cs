class MCTS {
    // The root node of the MCTS tree
    private Node root;

    // The maximum number of simulations to run for each move
    private int maxSimulations;

    public MCTS(int maxSimulations) {
        this.root = new Node();
        this.maxSimulations = maxSimulations;
    }

    // Function to determine the best move for the AI
    public int GetBestMove(GameState state) {
        // Clear the tree and set the root node to the current state
        root = new Node();
        root.state = state;

        // Run the MCTS algorithm for the maximum number of simulations
        for (int i = 0; i < maxSimulations; i++) {
            Node current = root;

            // Select a leaf node to expand
            while (!current.IsLeaf()) {
                current = current.Select();
            }

            // Expand the leaf node and simulate a random outcome
            current.Expand();
            int simulationResult = current.Simulate();

            // Backpropagate the result through the tree
            while (current != null) {
                current.Update(simulationResult);
                current = current.parent;
            }
        }

        // Return the move with the highest win rate
        return root.GetBestChild().move;
    }

    // Node class for the MCTS tree
    private class Node {
        // The state of the game represented by this node
        public GameState state;

        // The move that led to this state
        public int move;

        // The number of times this node has been visited
        public int visits;

        // The number of wins for the current player at this node
        public int wins;

        // The parent node of this node
        public Node parent;

        // The child nodes of this node
        public List<Node> children;

        // Function to expand the node by creating child nodes for all possible moves
        public void Expand() {
            // Generate a list of possible moves
            List<int> moves = state.GetPossibleMoves();

            // Create a child node for each move
            children = new List<Node>();
            foreach (int move in moves) {
                Node child = new Node();
                child.parent = this;
                child.move = move;
                children.Add(child);
            }
        }

        // Function to simulate a random outcome for the game state represented by this node
        public int Simulate() {
            // Clone the current game state
            GameState simulationState = state.Clone();

            // Perform a random sequence of moves until the game is over
            while (!simulationState.IsGameOver()) {
                List<int> moves = simulationState.GetPossibleMoves();
                int move = moves[UnityEngine.Random.Range(0, moves.Count)];
                simulationState.MakeMove(move);
            }

            // Return the result of the simulation
            return simulationState.GetResult();
        }

        // Function to update the node with the result of a simulation
        public void Update(int simulationResult) {
            visits++;
            if (simulationResult == 1)
